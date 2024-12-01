import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { TabProvider } from "../context/tab";
import { Layout, Tabs, Table } from "../global/components";
import { BrowserRouter } from "react-router-dom";
import { ToastContainer } from "react-toast";

const queryClient = new QueryClient();

function App() {
  return (
    <BrowserRouter>
      <Layout>
        <QueryClientProvider client={queryClient}>
          <TabProvider>
            <Tabs />
            <Table />
            <ToastContainer />
          </TabProvider>
        </QueryClientProvider>
      </Layout>
    </BrowserRouter>
  );
}

export default App;
