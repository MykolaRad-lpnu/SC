import { config, TabType } from "../../../config";
import { useTab } from "../../../context/tab";
import { Button } from "../Form/Field/button";

const linkStyles = [
  "inline-block p-4 border-b-2 border-transparent rounded-t-lg hover:text-gray-600 hover:border-gray-300 dark:hover:text-gray-300",
  "inline-block p-4 text-blue-600 border-b-2 border-blue-600 rounded-t-lg active dark:text-blue-500 dark:border-blue-500",
];

export const Tabs = () => {
  const { tab, setTab, setModalOpen } = useTab();

  return (
    <div className="text-sm font-medium text-center text-gray-500 border-b border-gray-200 dark:text-gray-400 dark:border-gray-700">
      <ul className="flex flex-wrap -mb-px w-full">
        {config.map(({ tabName }) => (
          <li
            className={
              "me-2 " + (tab !== tabName ? linkStyles[0] : linkStyles[1])
            }
            onClick={() => setTab(tabName as TabType)}
            key={tabName + "tab"}
          >
            {tabName}
          </li>
        ))}
        <div className="ml-auto self-center">
          <Button onClick={() => setModalOpen(true)}>Add new Entity</Button>
        </div>
      </ul>
    </div>
  );
};
