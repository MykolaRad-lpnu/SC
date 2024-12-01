import { ColumnDef, createColumnHelper } from "@tanstack/react-table";
import { toUpperFirstSumbol } from "../util";
import { Cell, DeleteCell } from "../global/components/Table/cell";
import { IColumnName } from "../types";

const columnHelper = createColumnHelper<any>();

export const getColumns = (
  names: (IColumnName | string)[]
): ColumnDef<any>[] => {
  const columns = genereteRecursiveColumns(names);
  columns.push({
    accessorKey: "actions",
    header: "",
    cell: DeleteCell,
    id: "actions",
    enableSorting: false,
  });
  return columns;
};

const genereteRecursiveColumns = (
  names: (IColumnName | string)[],
  parentName = ""
): ColumnDef<any>[] => {
  return names.map((item) => {
    if (typeof item === "object") {
      return columnHelper.group({
        id: parentName ? `${parentName}.${item.name}` : item.name,
        header: toUpperFirstSumbol([item.name])[0],
        columns: genereteRecursiveColumns(
          item.arr,
          parentName ? `${parentName}.${item.name}` : item.name
        ),
      });
    }

    return {
      accessorKey: parentName ? `${parentName}.${item}` : item,
      header: toUpperFirstSumbol([item])[0],
      cell: Cell,
      id: parentName ? `${parentName}.${item}` : item,
    };
  });
};
