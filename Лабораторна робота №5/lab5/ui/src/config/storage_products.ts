import { keepPreviousData, useQuery } from "@tanstack/react-query";
import { Api } from "../api";
import { baseUrl } from "../constants";
import { IConfigArrayItem } from "../types";
import * as yup from "yup";
import { Storages } from "./storage";
import { Products } from "./products";

export interface IStorageProduct {
  storageId: number;
  productId: number;
  quantity: number;
}

export const StorageProducts: IConfigArrayItem<
  IStorageProduct,
  IStorageProduct,
  IStorageProduct
> = {
  tabName: "Storage Products",
  api: new Api(`${baseUrl}StorageProduct`),
  tableConfig: {
    defaultColumns: ["storageId", "productId", "quantity"],
    mapToTable: (data = []) => data,
    mapBeforeUpdate: (data, columnName, newValue) => ({
      ...data,
      [columnName]: parseInt(newValue, 10), // Приведення до числа
    }),
    getIdFromRow: ({ storageId, productId }) => `${productId}/${storageId}`, // Унікальний ідентифікатор через комбінацію
  },
  formConfig: {
    fields: {
      storageId: {
        label: "Storage ID",
        as: "listbox",
        listboxProps: {},
        useGetOptions: () => {
          const { data } = useQuery({
            queryKey: [Storages.tabName],
            queryFn: () => Storages?.api?.getAll(),
            placeholderData: keepPreviousData,
          });
          return data?.map((storage) => ({
            value: storage.storageId as any,
            label: storage.name,
          }));
        },
      },
      productId: {
        label: "Product ID",
        as: "listbox",
        listboxProps: {},
        useGetOptions: () => {
          const { data } = useQuery({
            queryKey: [Products.tabName],
            queryFn: () => Products?.api?.getAll(),
            placeholderData: keepPreviousData,
          });
          return data?.map((storage) => ({
            value: storage.productId as any,
            label: storage.name,
          }));
        },
      },
      quantity: {
        label: "Quantity",
        as: "input",
        inputProps: {
          type: "number",
          step: 1,
        },
      },
    },
    yupSchema: yup.object().shape({
      storageId: yup
        .number()
        .required("Storage ID is required")
        .positive("Storage ID must be a positive number")
        .integer("Storage ID must be an integer"),
      productId: yup
        .number()
        .required("Product ID is required")
        .positive("Product ID must be a positive number")
        .integer("Product ID must be an integer"),
      quantity: yup
        .number()
        .required("Quantity is required")
        .min(0, "Quantity cannot be negative")
        .integer("Quantity must be an integer"),
    }),
    beforeSendToBackend: (data) => data,
    formTitle: "Storage Product Form",
  },
};
