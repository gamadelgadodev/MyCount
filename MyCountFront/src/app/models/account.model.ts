export interface Account {
    id: number;
    user: string | null;
    userId: number;
    name: string;
    balance: number;
    description: string;
    debt: number;
    isDeleted: boolean;
  }