export interface SimpleCrudItem {
  id: number;
  name: string;
  description: string;
  createdDate: Date;
  isActive: boolean;
}

export interface CreateSimpleCrudItem {
  name: string;
  description: string;
  isActive: boolean;
}

export interface UpdateSimpleCrudItem {
  id: number;
  name: string;
  description: string;
  isActive: boolean;
}