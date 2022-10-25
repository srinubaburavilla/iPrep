import axios from "axios";
import moment from "moment";
import { useEffect, useState } from "react";
import { Button, Container, Row } from "react-bootstrap";
import { Subject } from "../../Models/Subject";

function ReportManager() {

    const [allQuestionsAsJson, setAllQuestionsAsJson] = useState([])

    const reportsApiBaseUrl = "http://localhost:7253/Reports";

    function loadAllQuestions() {
        axios
          .get(`${reportsApiBaseUrl}\\allsubjectswithquestionsandanswers`)
          .then((response) => {
            setAllQuestionsAsJson(response.data);
          })
          .catch((error) => console.log(error));
      }

      useEffect(() => {
        loadAllQuestions();
      }, []);

    const exportData = () => {

        const timestamp = moment().format('YYYY-MM-DD_HH-ms-SSS');

        const jsonString = `data:text/json;chatset=utf-8,${encodeURIComponent(
          JSON.stringify(allQuestionsAsJson)
        )}`;
        const link = document.createElement("a");
        link.href = jsonString;
        link.download = `iPrep-AllQuestions-Exported-File-${timestamp}.json`;
    
        link.click();
      };

  return (
    <>
      <Container className="d-flex justify-content-center mb-3 text-center mt-sm-5">
        <Row lg={8}>
          <Button variant="secondary" size="lg" active onClick={exportData} >
            Export All Questions As JSON
          </Button>
        </Row>
      </Container>
    </>
  );
}

export default ReportManager;
