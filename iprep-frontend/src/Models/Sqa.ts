export class Sqa {
  id: number;
  answer: string;
  question: string;
  constructor(id: number, question: string, answer: string) {
    this.id = id;
    this.question = question;
    this.answer = answer;
  }
}
