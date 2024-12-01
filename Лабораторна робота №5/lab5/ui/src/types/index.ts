import { Api } from "../api";
import { Checkbox } from "../global/components/Form/Field/checkbox";
import { ComponentProps } from "react";
import { Schema } from "yup";
import { Input } from "../global/components/Form/Field/input";
import { Select } from "../global/components/Form/Field/select";
import { Textarea } from "../global/components/Form/Field/textarea";
import { Listbox } from "../global/components/Form/Field/listbox";

export type IColumnName<Row> =
  | keyof Row
  | {
      [K in keyof Row]: Row[K] extends object
        ? { name: K; arr: IColumnName<Row[K]>[] }
        : never;
    }[keyof Row];

export type BeckendResponseProps = {
  response: any;
  setError: (name: string, error: { message: string }) => void;
  clearErrors: (name: string) => void;
  reset: () => void;
  closeModal: () => void;
  refetch?: () => void;
  toast: typeof import("react-toast").toast;
};

export type TableConfigProps<Data, Row> = {
  mapToTable: (data?: Data[]) => Row[];
  mapBeforeUpdate: (
    data: Row,
    columnName: string,
    newValue: string
  ) => Partial<Data>;
  getIdFromRow: (row: Row) => string;
  defaultColumns: IColumnName<Row>[];
};

export type FormConfigProps<Form> = {
  fields: {
    [K in keyof Partial<Form>]: {
      label: string;
      as: "input" | "select" | "textarea" | "checkbox" | "listbox";
      inputProps?: ComponentProps<typeof Input>;
      selectProps?: ComponentProps<typeof Select>;
      textareaProps?: ComponentProps<typeof Textarea>;
      checkboxProps?: ComponentProps<typeof Checkbox>;
      listboxProps?: ComponentProps<typeof Listbox>;
      useGetOptions?: () => { value: string; label: string }[] | undefined;
    };
  };
  yupSchema: Schema<Partial<Form>>;
  beforeSendToBackend?: (data: Partial<Form>) => any;
  handleBeckendResponse?: (props: BeckendResponseProps) => void;
  formTitle: string;
};

export type IConfigArrayItem<Data, Row, Form> = {
  tabName: string;
  api: Api<Data>;
  tableConfig: TableConfigProps<Data, Row>;
  formConfig: FormConfigProps<Form>;
};
