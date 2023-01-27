export interface Warehouse {
  code?: string;
  name?: string;
  nickname?: string;
  branch?: string;
  address?: string;
  type?: string;
  group?: string;
  carryOutFlag?: number;
  stocktakingFlag?: number;
  departmentCode?: string;
  profitCode?: string;
  costCenter?: string;
  documentCode?: string;
  policeNumber?: number;
  fifoFlag?: number;
  fifoDays?: number;
  transferModelFlag?: number;
  palletGroup?: string;
  qualityFlag?: number;
  remark?: string;
  system?: null;
  status?: null;
  recordStatus?: number;
//   createdTime?: Date;
//   createdUser?: string;
//   changedTime?: Date;
//   changedUser?: string;
}
