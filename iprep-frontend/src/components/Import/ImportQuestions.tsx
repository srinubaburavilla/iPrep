import axios from "axios";
import { useEffect, useState, useRef } from "react";
import { Button, Col, Container, Form, Row } from "react-bootstrap";
import IPrepToast from "../Shared/IPrepToast";

function ImportQuestions() {
  const [fileName, setfileName] = useState("");
  const [uploadedFile, setUploadedFile] = useState("");
  const [showToast, setShowToast] = useState(false);
  const focusPoint = useRef<any>(null);
  const ImportApiBaseUrl = "http://localhost:7253/Import";

  useEffect(() => {
    console.log("useEffect called...");
  }, []);

  const setSelectedFileDetails = (e: any) => {
    setfileName(e.target.files[0].name);
    setUploadedFile(e.target.files[0]);
  };

  const ImportFile = async (e: any) => {
    const formData = new FormData();
    if (fileName) {
      formData.append("fileName", fileName);
    }
    if (uploadedFile) {
      formData.append("file", uploadedFile);
    }
    await axios
      .post(`${ImportApiBaseUrl}\\uploadquestionsfile`, formData)
      .then((response) => { 
        if (focusPoint.current) {
            focusPoint.current.value = ''
        }   
        setfileName("");
        setUploadedFile("");
        setShowToast(true);
        setTimeout(() => {
          setShowToast(false);
        }, 2000);
      })
      .catch((error) => console.log(error));
  };

  return (
    <>
      <Container className="p-3">
        <Row className="justify-content-center">
          <Col xs={7} md={7}>
            {showToast && (
              <IPrepToast
                header="Success"
                body="Questions Imported Successfully"
              />
            )}
          </Col>
          <Col xs={7} md={7}>
            <div className="row mt-3">
              <h2>Import Questions</h2>
            </div>
          </Col>
          <Col xs={7} md={7}>
            <Form.Group controlId="formFile" className="mb-3">
              <Form.Label>Select File to Import</Form.Label>
              <Form.Control type="file" as="input" onChange={setSelectedFileDetails} ref={focusPoint} />
            </Form.Group>
          </Col>
          <Col xs={7} md={7}>
            <Button
              variant="primary"
              className="mt-3 mr-3"
              onClick={ImportFile}
            >
              Import
            </Button>
            <Button variant="danger" className="mt-3">
              Clear
            </Button>
          </Col>
        </Row>
      </Container>
    </>
  );
}

export default ImportQuestions;
