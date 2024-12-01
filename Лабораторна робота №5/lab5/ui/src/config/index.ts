/* eslint-disable @typescript-eslint/no-explicit-any */
import { IConfigArrayItem } from "../types";
import { Products } from "./products";
import { Storages } from "./storage";
import { StorageKeepers } from "./storage_keepers";
import { StorageProducts } from "./storage_products";

// Enum for Tabs
export enum Tabs {
  Products = "Products",
  Storages = "Storages",
  StorageKeepers = "StorageKeepers",
  StorageProducts = "StorageProducts",
}

// Configuration for each tab with their respective types based on IConfigArrayItem
export const config: IConfigArrayItem<any, any, any>[] = [
  Products,
  Storages,
  StorageKeepers,
  StorageProducts,
];

// Type for TabType using the Tabs enum
export type TabType = keyof typeof Tabs;
