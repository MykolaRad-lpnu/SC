import { CellContext, Row, RowData } from "@tanstack/react-table";
import { useEffect, useState } from "react";
import { Button } from "../Form/Field/button";

declare module "@tanstack/react-table" {
  interface TableMeta<TData extends RowData> {
    updateData: (row: Row<TData>, columnId: string, value: unknown) => void;
    deleteData: (row: Row<TData>) => void;
  }
}

export const Cell = ({
  getValue,
  row,
  column: { id },
  table,
}: CellContext<any, unknown>) => {
  const initialValue = getValue();
  const [value, setValue] = useState(initialValue);

  const onBlur = () => {
    table.options.meta?.updateData(row, id, value);
  };

  useEffect(() => {
    setValue(initialValue);
  }, [initialValue]);

  return (
    <input
      value={value as string}
      onChange={(e) => setValue(e.target.value)}
      onBlur={onBlur}
      className="focus:bg-gray-50 focus:border text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
    />
  );
};

export const DeleteCell = ({ table, row }: CellContext<any, unknown>) => (
  <Button color="red" onClick={() => table.options.meta?.deleteData(row)}>
    🗑
  </Button>
);
