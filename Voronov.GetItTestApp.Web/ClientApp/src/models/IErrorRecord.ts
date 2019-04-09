export interface IErrorRecord {
    Id: number;
    InputDate: Date;
    ShortDescription: string;
    FullDescription: string;
    OwnerId: number;
    Status: number;
    Urgency: number;
    CriticalType: number;
}