export class Category {
  constructor(public id: number,
    public title: string,
    public description: string,
    public publishDate: Date,
    public PublishedById: number
    ) { }
}
