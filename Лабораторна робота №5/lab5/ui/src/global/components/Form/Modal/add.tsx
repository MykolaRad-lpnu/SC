import { Dialog, DialogBody, DialogTitle } from ".";
import { MyForm } from "..";
import { IConfigArrayItem } from "../../../../types";

type AddModalProps = {
  isModalOpen: boolean;
  closeModal: () => void;
  selectedTab?: IConfigArrayItem<any, any, any>;
  onSubmit: (data: any) => Promise<any>;
  refetch: () => void;
};

export function AddModal({
  isModalOpen,
  closeModal,
  selectedTab,
  onSubmit,
  refetch,
}: AddModalProps) {
  if (!selectedTab) return null;
  return (
    <Dialog open={isModalOpen} onClose={closeModal} size="sm">
      <DialogTitle>{selectedTab?.formConfig.formTitle}</DialogTitle>
      <DialogBody>
        <MyForm
          {...selectedTab?.formConfig}
          onSubmit={onSubmit}
          closeModal={closeModal}
          refetch={refetch}
        />
      </DialogBody>
    </Dialog>
  );
}
