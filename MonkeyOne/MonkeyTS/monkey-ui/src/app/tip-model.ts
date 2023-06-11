export interface MonkeyUI{
    Message:string;
    Status: string,
    TipDetail: MonkeyTable[];
}

export interface MonkeyTable {
    
    tip: string | null;
}