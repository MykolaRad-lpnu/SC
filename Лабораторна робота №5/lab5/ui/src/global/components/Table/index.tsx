import { Pagination } from "./paggination";
import DataTable from "./table";
import { usePaternTable } from "../../../hook/usePaternTable";
import { AddModal } from "../Form/Modal/add";
import { DeleteModal } from "../Form/Modal/delete";
import { useCallback, useMemo } from "react";

const divStyle =
  "font-serif text-center text-4xl rounded-2xl border border-1 border-black content-center";

export const Table = () => {
  const {
    closeModal,
    error,
    isLoading,
    isModalOpen,
    onSubmit,
    refetch,
    table,
    selectedTab,
    tab,
    closeModalDeleteModal,
    isDeleteModalOpen,
    onDelete,
  } = usePaternTable();

  const content = () => {
    if (!selectedTab)
      return <div className={divStyle}>Please chose correct tab</div>;

    if (isLoading) return <div className={divStyle}>Loading...</div>;

    if (error) return <div className={divStyle}>Error: {error.message}</div>;

    return (
      <>
        <DataTable table={table} tab={tab} />
        <Pagination table={table} />
      </>
    );
  };

  return (
    <>
      <AddModal
        {...{ isModalOpen, closeModal, selectedTab, onSubmit, refetch }}
      />

      <DeleteModal
        closeModal={closeModalDeleteModal}
        isModalOpen={isDeleteModalOpen}
        onSubmit={onDelete}
      />

      {content()}
    </>
  );
};
