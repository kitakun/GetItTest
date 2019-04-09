export enum ErrorStatus {
    New = 0,
    Opened = 1,
    Resolved = 2,
    Completed = 3
}

export enum ErrorUrgency {
    NonEmergent = 0,
    NotUrgent = 10,
    Urgent = 20,
    VeryUrgent = 30
}

export enum ErrorImportanceType {
    Accident = 0,
    Critical = 1,
    NonCritical = 2,
    ChangeRequest = 3
}