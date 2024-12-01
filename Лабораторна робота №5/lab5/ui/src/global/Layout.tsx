import { ReactNode } from "react";

type LayoutProps = {
  children: ReactNode;
};

export const Layout = ({ children }: LayoutProps) => {
  return (
    <div className="h-screen p-2 grid grid-rows-[auto_1fr_auto]">
      {children}
    </div>
  );
};
