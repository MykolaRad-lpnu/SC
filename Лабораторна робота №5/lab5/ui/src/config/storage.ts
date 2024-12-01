import { Api } from "../api";
import { baseUrl } from "../constants";
import { IConfigArrayItem } from "../types";
import * as yup from "yup";
import { IStorageProduct } from "./storage_products";
import { IStorageKeeper } from "./storage_keepers";

export interface IStorage {
  storageId: number;
  name: string;
  region: string;
  city: string;
  address: string;
}

export interface IStorageDetails extends IStorage {
  storageProducts: IStorageProduct[];
  storageKeepers: IStorageKeeper[];
}

interface TableRow extends IStorage {
  storageProducts: string;
  storageKeepers: string;
}

export const Storages: IConfigArrayItem<
  IStorageDetails,
  TableRow,
  Omit<IStorage, "storageId">
> = {
  tabName: "Storages",
  api: new Api(`${baseUrl}Storage`),
  tableConfig: {
    defaultColumns: ["name", "region", "city", "address"],
    mapToTable: (data = []) =>
      data.map((e) => ({
        ...e,
        storageProducts: JSON.stringify(e.storageProducts),
        storageKeepers: JSON.stringify(e.storageKeepers),
      })),
    mapBeforeUpdate: (data, columnName, newValue) => ({
      ...data,
      storageProducts: [],
      storageKeepers: [],
      [columnName]: newValue,
    }),
    getIdFromRow: ({ storageId }) => storageId.toString(),
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
      region: {
        label: "Region",
        as: "input",
        inputProps: {
          type: "text",
        },
      },
      city: {
        label: "City",
        as: "input",
        inputProps: {
          type: "text",
        },
      },
      address: {
        label: "Address",
        as: "textarea",
        textareaProps: {
          rows: 3,
        },
      },
    },
    yupSchema: yup.object().shape({
      name: yup.string().required("Name is required"),
      region: yup.string().required("Region is required"),
      city: yup.string().required("City is required"),
      address: yup.string().required("Address is required"),
    }),
    beforeSendToBackend: (data) => data,
    formTitle: "Storage Form",
  },
};
