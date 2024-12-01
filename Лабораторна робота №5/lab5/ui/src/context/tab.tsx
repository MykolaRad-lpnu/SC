import { createContext, useContext, useState } from "react";
import { useSearchParams } from "react-router-dom";
import { TabType } from "../config";

interface ITabContext {
  tab: TabType;
  setTab: (tab: TabType) => void;
  isModalOpen: boolean;
  setModalOpen: (isOpen: boolean) => void;
}

const TabContext = createContext<ITabContext>({
  tab: "" as TabType,
  setTab: () => {},
  isModalOpen: false,
  setModalOpen: () => {},
});

const TabProvider = ({ children }: { children: React.ReactNode }) => {
  const [searchParams, setSearchParams] = useSearchParams();
  const [isModalOpen, setModalOpen] = useState(false);
  const tab = (searchParams.get("tab") || "") as TabType;

  const setTab = (newTab: TabType) => {
    setSearchParams({ tab: newTab });
  };

  return (
    <TabContext.Provider value={{ tab, setTab, isModalOpen, setModalOpen }}>
      {children}
    </TabContext.Provider>
  );
};

const useTab = () => useContext(TabContext);

export { TabProvider, useTab };
