import { keepPreviousData, useQuery } from "@tanstack/react-query";
import { config } from "../config";
import { useTab } from "../context/tab";
import { useCallback, useEffect, useMemo, useState } from "react";
import { useSkipper } from "./useSkipper";
import { getColumns } from "./getColumns";
import {
  getCoreRowModel,
  getFilteredRowModel,
  getPaginationRowModel,
  getSortedRowModel,
  useReactTable,
} from "@tanstack/react-table";
import { get, set } from "../util";
import { toast } from "react-toast";

export const usePaternTable = () => {
  const { tab, isModalOpen, setModalOpen } = useTab();
  const [deleteId, setDeleteId] = useState<string | null | undefined>(null);

  const selectedTab = useMemo(
    () => config.find(({ tabName }) => tab === tabName),
    [tab]
  );

  const { data, isLoading, error, refetch } = useQuery({
    queryKey: [tab],
    queryFn: () => selectedTab?.api?.getAll(),
    placeholderData: keepPreviousData,
    enabled: !!selectedTab,
  });

  const [memoData, setMemoData] = useState(data);
  const [autoResetPageIndex, skipAutoResetPageIndex] = useSkipper();

  useEffect(() => {
    setMemoData(selectedTab?.tableConfig?.mapToTable(data) || []);
  }, [data, selectedTab]);

  const columns = useMemo(
    () => getColumns(selectedTab?.tableConfig?.defaultColumns || []),
    [selectedTab?.tableConfig?.defaultColumns]
  );

  const table = useReactTable({
    columns: columns || [],
    data: memoData,
    getCoreRowModel: getCoreRowModel(),
    getFilteredRowModel: getFilteredRowModel(),
    getPaginationRowModel: getPaginationRowModel(),
    getSortedRowModel: getSortedRowModel(),
    autoResetPageIndex,
    enableMultiSort: true,
    meta: {
      updateData: (row, columnId, value) => {
        try {
          const previousData = get(row.original, columnId);
          const body = selectedTab?.tableConfig?.mapBeforeUpdate(
            row.original,
            columnId,
            value as string
          );
          console.log(body, previousData, columnId, value);
          if (previousData === value) return;
          setMemoData((prev) => {
            set(prev[row.index], columnId, value);
            return prev;
          });

          const id = selectedTab?.tableConfig?.getIdFromRow(row.original);

          selectedTab?.api
            ?.put(id!, body as any)
            .then(() => {
              toast.success("Data updated successfully");
            })
            .catch((err) => {
              setMemoData((prev) => {
                set(prev[row.index], columnId, previousData);
                return prev;
              });
              toast.error("Something went wrong");
              console.error(err);
            });

          skipAutoResetPageIndex();
        } catch (err) {
          toast.error(err.message);
          return;
        }
      },
      deleteData: (row) => {
        const id = selectedTab?.tableConfig?.getIdFromRow(row.original);

        setDeleteId(id);
      },
    },
  });

  const onSubmit = useCallback(
    async (data) => selectedTab?.api.post(data),
    [selectedTab]
  );
  const onDelete = useCallback(async () => {
    const res = await selectedTab?.api.delete(deleteId as string);
    setMemoData((prev) =>
      prev.filter(
        (row) => selectedTab?.tableConfig?.getIdFromRow(row) !== deleteId
      )
    );
    return res;
  }, [selectedTab, deleteId]);

  const closeModal = useCallback(() => setModalOpen(false), [setModalOpen]);
  const closeModalDeleteModal = useCallback(() => setDeleteId(null), []);
  return {
    table,
    isLoading,
    error,
    refetch,
    isModalOpen,
    closeModal,
    onSubmit,
    selectedTab,
    tab,
    isDeleteModalOpen: !!deleteId,
    closeModalDeleteModal,
    onDelete,
  };
};
