export class Subject {
  id: number;
  subject: string;
  created: Date;

  constructor(id: number, subject: string, created: Date) {
    this.id = id;
    this.subject = subject;
    this.created = created;
  }
}
