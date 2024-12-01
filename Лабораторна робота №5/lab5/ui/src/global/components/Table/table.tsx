import { flexRender, Table as TanstackTable } from "@tanstack/react-table";
import { GetHeader } from "./header";

type TableProps = {
  table: TanstackTable<any>;
  tab: string;
};

const Table = ({ table, tab }: TableProps) => {
  return (
    <div className="relative overflow-x-auto shadow-md sm:rounded-lg no-scrollbar my-2">
      <table
        key={tab}
        className="w-full text-sm text-left rtl:text-right text-gray-500 dark:text-gray-400"
      >
        {GetHeader(table)}
        <tbody>
          {table.getRowModel().rows.map((row) => {
            return (
              <tr
                key={row.id}
                className="bg-white border-b dark:bg-gray-800 dark:border-gray-700"
              >
                {row.getVisibleCells().map((cell) => {
                  return (
                    <td
                      key={cell.id}
                      className="px-6 py-4 font-medium text-gray-900 whitespace-nowrap dark:text-white"
                    >
                      {flexRender(
                        cell.column.columnDef.cell,
                        cell.getContext()
                      )}
                    </td>
                  );
                })}
              </tr>
            );
          })}
        </tbody>
      </table>
    </div>
  );
};


export default Table;