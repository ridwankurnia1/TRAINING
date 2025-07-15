// Variable.ts
export interface VariableItem {
  variableId?: number;
  user: string;
  name: string;
  code: string;
  value: string;
  cretedTime: Date;
}
export interface CreateVariableItem {
  user: string;
  name: string;
  code: string;
  value: string;
}
export interface UpdateVariableItem {
  variableId?: number;
  user: string;
  name: string;
  code: string;
  value: string;
}