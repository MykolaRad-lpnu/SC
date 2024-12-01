export interface IApi<TData> {
  getAll: () => Promise<TData[]>;
  getById: (id: string) => Promise<TData>;
  post: (data: TData) => Promise<TData>;
  put: (id: string, data: Partial<TData>) => Promise<TData>;
  delete: (id: string) => Promise<TData>;
  patch: (id: string, data: Partial<TData>) => Promise<TData>;
}

export class Api<TData> implements IApi<TData> {
  constructor(private baseUrl: string) {}

  async getAll() {
    const response = await fetch(this.baseUrl);
    return returnJSON(response) as Promise<TData[]>;
  }

  async getById(id: string) {
    const response = await fetch(`${this.baseUrl}/${id}`);
    return returnJSON(response) as Promise<TData>;
  }

  async post(data: TData) {
    const response = await fetch(this.baseUrl, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(data),
    });
    return returnJSON(response) as Promise<TData>;
  }

  async put(id: string, data: Partial<TData>) {
    const response = await fetch(`${this.baseUrl}/${id}`, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(data),
    });
    return returnJSON(response) as Promise<TData>;
  }

  async delete(id: string) {
    const response = await fetch(`${this.baseUrl}/${id}`, {
      method: "DELETE",
    });
    return returnJSON(response) as Promise<TData>;
  }

  async patch(id: string, data: Partial<TData>) {
    const response = await fetch(`${this.baseUrl}/${id}`, {
      method: "PATCH",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(data),
    });
    return returnJSON(response) as Promise<TData>;
  }
}

async function returnJSON<T>(response: Response): Promise<T> {
  if (!response.ok) {
    throw new Error((await response.json()) || response.statusText);
  }

  if (response.headers.get("content-type")?.includes("application/json")) {
    return response.json();
  } else if (response.headers.get("content-type")?.includes("text/plain")) {
    return response.text() as unknown as T;
  } else {
    return response.status !== 204
      ? response?.json() || response.text()
      : response;
  }
}
