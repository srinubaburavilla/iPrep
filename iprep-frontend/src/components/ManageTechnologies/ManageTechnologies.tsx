import { useEffect, useState } from "react";
import axios from "axios";
import "react-bootstrap";
import { Button, Col, Container, Form, Row, Table } from "react-bootstrap";
import { Subject } from "../../Models/Subject";
import IPrepToast from "../Shared/IPrepToast";
import moment from "moment";

function ManageTechnologies() {
  const technologiesApiBaseUrl = "http://localhost:7253/Subjects";
  const [technologies, setTechnologies] = useState<Subject[]>([]);
  const [technology, setTechnology] = useState("");
  const [showToast, setshowToast] = useState(false);
  const [toastBody, setToastBody] = useState('');

  useEffect(() => {
    loadTechnologies();
  }, []);

  function loadTechnologies() {
    axios
      .get(`${technologiesApiBaseUrl}\\all`)
      .then((response) => {
        setTechnologies(response.data);
      })
      .catch((error) => console.log(error));
  }

  function addNewTechnology() {
    axios
      .post(`${technologiesApiBaseUrl}\\add`, {
        subject: technology,
      })
      .then((response) => {
        loadTechnologies();
        setToastBody(`New technology (${technology}) added successfully`)
        setshowToast(true);
        setTimeout(() => {
          setshowToast(false)
        }, 2000);
        clearForm();
        window.scrollTo(0, 0);
      })
      .catch((error) => console.log(error));
  }

  function clearForm() {
    setTechnology("");
  }

  function deleteTechnology(id:number, subject:string){
    axios
    .delete(`${technologiesApiBaseUrl}\\delete\\${id}`,)
    .then((response) => {
      loadTechnologies();
        setToastBody(`Technology (${subject}) deleted successfully`)
        setshowToast(true);
        setTimeout(() => {
          setshowToast(false)
        }, 2000);
        window.scrollTo(0, 0);
    })
    .catch((error) => console.log(error));
  }

  return (
    <>
      <Container className="p-3">
        {showToast && (
          <IPrepToast
            header="Success"
            body={toastBody}
            setshowToast={setshowToast}
          />
        )}
        <Row lg={8}>
          <Col>
            <div className="row mt-3">
              <h2>Add New Technology</h2>
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
                placeholder="Technology"
                onChange={(event) => {
                  setTechnology(event.target.value);
                }}
                value={technology}
              ></Form.Control>
            </div>
          </Col>
        </Row>
        <Row lg={8}>
          <Col>
            <Button
              variant="primary"
              className="mt-3 mr-3"
              onClick={addNewTechnology}
            >
              Submit
            </Button>
            <Button variant="danger" className="mt-3" onClick={clearForm}>
              Clear
            </Button>
          </Col>
        </Row>
      </Container>
      <Container className="p-3">
        <Table striped>
          <thead>
            <tr>
              <th>#</th>
              <th>Technology</th>
              <th>Created On</th>
              <th>Action</th>
            </tr>
          </thead>
          <tbody>
            {technologies.map((x) => (
              <tr>
                <td>{x.id}</td>
                <td>{x.subject}</td>
                <td>{moment(x.created).format('MMMM Do, YYYY')}</td>
                <td>
                  <Button variant="danger" className="mt-3" onClick={e=> deleteTechnology(x.id,x.subject) }>
                    Delete
                  </Button>
                </td>
              </tr>
            ))}
          </tbody>
        </Table>
      </Container>
    </>
  );
}

export default ManageTechnologies;
