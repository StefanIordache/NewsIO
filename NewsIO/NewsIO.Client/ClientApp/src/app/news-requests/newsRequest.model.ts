export class NewsRequest {
  constructor(public id: number,
    public title: string,
    public description: string,
    public status: string,
    public isClosed: Boolean,
    public requestDate: Date,
    public requestedById: string,
    public requestedBy: string,
    public categoryId: number
  ) { }
}
