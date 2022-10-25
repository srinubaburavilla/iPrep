import { useEffect, useState } from "react";
import axios from "axios";
import "react-bootstrap";
import { Button, Col, Container, Form, Row, Toast } from "react-bootstrap";
import { Subject } from "../../Models/Subject";
import IPrepToast from "../Shared/IPrepToast";
import ReactQuill from 'react-quill';
import 'react-quill/dist/quill.snow.css';

function AddQuestions() {
  const AddQuestionsApiBaseUrl = "http://localhost:7253/ManageQuestions";
  const subjectsApiBaseUrl = "http://localhost:7253/Subjects";
  const [subjects, setSubjects] = useState<Subject[]>([]);
  const [subjectId, setSubjectId] = useState(0);
  const [question, setQuestion] = useState("");
  const [answer, setAnswer] = useState("");
  const [showToast, setshowToast] = useState(false);

  const  modules  = {
    toolbar: [
        [{ font: [] }],
        [{ header: [1, 2, 3, 4, 5, 6, false] }],
        ["bold", "italic", "underline", "strike"],
        [{ color: [] }, { background: [] }],
        [{ script:  "sub" }, { script:  "super" }],
        ["blockquote", "code-block"],
        [{ list:  "ordered" }, { list:  "bullet" }],
        [{ indent:  "-1" }, { indent:  "+1" }, { align: [] }],
        ["link", "image", "video"],
        ["clean"],
    ],
    clipboard: {
      matchVisual: false,
      matchers: [
      ]
    }
};

  let formats = [
    'header', 'font', 'size',
    'bold', 'italic', 'underline', 'strike', 'blockquote',
    'list', 'bullet', 'indent',
    'link', 'image', 'video'
  ]

  useEffect(() => {
    axios
      .get(`${subjectsApiBaseUrl}\\all`)
      .then((response) => setSubjects(response.data))
      .catch((error) => console.log(error));
  }, []);

  function addNewQuestion() {
    axios
      .post(`${AddQuestionsApiBaseUrl}\\addnew`, {
        subjectId: subjectId,
        question: question,
        answer: answer,
      })
      .then((response) =>
      {
        setshowToast(true);
        setTimeout(() => {
          setshowToast(false)
        }, 2000);
        clearForm();
        window.scrollTo(0, 0);
      })
      .catch((error) => console.log(error));
  }

  function clearForm()
  {
    setSubjectId(0);
    setQuestion('');        
    setAnswer('');  
  }

  return (
    <>
      <Container className="p-3">
         {showToast && <IPrepToast header = "Success" body = "New question added successfully" />} 
        <Row lg={8}>
          <Col>
            <div className="row mt-3">
              <h2>Add New Question</h2>
            </div>
          </Col>
        </Row>
        <Row lg={8}>
          <Col>
            <div className="row mt-3">
              <Form.Control
                as="select"
                aria-label="Default select example"
                onChange={(event) => {
                  setSubjectId(parseInt(event.target.value));
                }}
                value = {subjectId}
              >
                <option value="0">Select Technology</option>
                {subjects.map((x) => (
                  <option value={x.id}>{x.subject}</option>
                ))}
              </Form.Control>
            </div>
          </Col>
        </Row>
        <Row lg={8}>
          <Col>
            <div className="row mt-3">
              <Form.Control
                as="textarea"
                className="form-control h-25"
                rows={2}
                placeholder="Question"
                onChange={(event) => {
                  setQuestion(event.target.value);
                }} value={question}
              ></Form.Control>
            </div>
          </Col>
        </Row>
        {/* <Row lg={8}>
          <Col>
            <div className="row mt-3">
              <Form.Control
                as="textarea"
                className="form-control h-25"
                rows={10}
                placeholder="Answer"
                onChange={(event) => {
                  setAnswer(event.target.value);
                }} value = {answer}
              ></Form.Control>
            </div>
          </Col>
        </Row> */}
         <Row lg={8}>
          <Col>
            <div className="row mt-3">
            <ReactQuill theme="snow" value={answer} modules={modules} onChange={setAnswer} placeholder="Answer...." />
            </div>
          </Col>
        </Row>
        <br/>
        <br/>
        <Row lg={8}>
          <Col>
              <Button variant="primary" className="mt-3 mr-3" onClick={addNewQuestion}>
                Submit
              </Button>
              <Button variant="danger" className="mt-3" onClick={clearForm} >Clear</Button>
          </Col>
        </Row>
      </Container>
    </>
  );
}

export default AddQuestions;
