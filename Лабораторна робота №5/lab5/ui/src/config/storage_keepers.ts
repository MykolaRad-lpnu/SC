import { Api } from "../api";
import { baseUrl } from "../constants";
import { IConfigArrayItem } from "../types";
import * as yup from "yup";
import { Storages } from "./storage";
import { keepPreviousData, useQuery } from "@tanstack/react-query";

export interface IStorageKeeper {
  storageKeeperId: number;
  storageId: number;
  firstName: string;
  lastName: string;
  phone: string;
}

export const StorageKeepers: IConfigArrayItem<
  IStorageKeeper,
  IStorageKeeper,
  Omit<IStorageKeeper, "storageKeeperId">
> = {
  tabName: "Storage Keepers",
  api: new Api(`${baseUrl}StorageKeeper`),
  tableConfig: {
    defaultColumns: ["firstName", "lastName", "phone", "storageId"],
    mapToTable: (data = []) => data,
    mapBeforeUpdate: (data, columnName, newValue) => ({
      ...data,
      [columnName]: newValue,
    }),
    getIdFromRow: ({ storageKeeperId }) => storageKeeperId.toString(),
  },
  formConfig: {
    fields: {
      firstName: {
        label: "First Name",
        as: "input",
        inputProps: {
          type: "text",
        },
      },
      lastName: {
        label: "Last Name",
        as: "input",
        inputProps: {
          type: "text",
        },
      },
      phone: {
        label: "Phone",
        as: "input",
        inputProps: {
          type: "text",
        },
      },
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
          return data?.map((account) => ({
            value: account.storageId as any,
            label: account.name,
          }));
        },
      },
    },
    yupSchema: yup.object().shape({
      firstName: yup.string().required("First name is required"),
      lastName: yup.string().required("Last name is required"),
      phone: yup
        .string()
        .required("Phone number is required")
        .matches(
          /^\+?[1-9]\d{1,14}$/,
          "Phone number must be a valid international format"
        ),
      storageId: yup
        .number()
        .required("Storage ID is required")
        .positive("Storage ID must be a positive number")
        .integer("Storage ID must be an integer"),
    }),
    beforeSendToBackend: (data) => data,
    formTitle: "Storage Keeper Form",
  },
};
