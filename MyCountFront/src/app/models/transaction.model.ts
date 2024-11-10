export interface Transaction {
    accountId: number;
    value: number;
    description: string;
    typeTransaction: string;
    date: string;
    transactionCatId: number;
    nessesary: boolean | null;
}
