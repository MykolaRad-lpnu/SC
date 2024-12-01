import { Api } from "../api";
import { baseUrl } from "../constants";
import { IConfigArrayItem } from "../types";
import * as yup from "yup";
import { IStorageProduct } from "./storage_products";

export interface IProduct {
  productId: number;
  name: string;
  unit: string;
  price: number;
}

export interface IProductDetails extends IProduct {
  storageProducts: IStorageProduct[];
}

export const Products: IConfigArrayItem<
  IProductDetails,
  IProductDetails,
  Omit<IProduct, "productId">
> = {
  tabName: "Products",
  api: new Api(`${baseUrl}Product`),
  tableConfig: {
    defaultColumns: ["name", "unit", "price"],
    mapToTable: (data = []) => data,
    mapBeforeUpdate: (data, columnName, newValue) => ({
      ...data,
      [columnName]: newValue,
    }),
    getIdFromRow: ({ productId }) => productId.toString(),
  },
  formConfig: {
    fields: {
      name: {
        label: "Name",
        as: "input",
        inputProps: {
          type: "text",
        },
      },
      unit: {
        label: "Unit",
        as: "input",
        inputProps: {
          type: "text",
        },
      },
      price: {
        label: "Price",
        as: "input",
        inputProps: {
          type: "number",
          step: "0.01",
        },
      },
    },
    yupSchema: yup.object().shape({
      name: yup.string().required("Name is required"),
      unit: yup.string().required("Unit is required"),
      price: yup
        .number()
        .required("Price is required")
        .min(0, "Price must be a positive value"),
    }),
    beforeSendToBackend: (data) => data,
    formTitle: "Product Form",
  },
};
