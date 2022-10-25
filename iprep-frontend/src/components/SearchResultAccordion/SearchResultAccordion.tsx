import Accordion from "react-bootstrap/Accordion";
import { SearchResponse } from "../../Models/SearchResponse";
import parse from "html-react-parser";
import { BsFillTrashFill } from "react-icons/bs";
import { Container, Row, Col } from "react-bootstrap";

function SearchResultAccordion(props: any) {
  let accordiontabs = document.querySelectorAll(".accordion .accordion-item svg");
  accordiontabs.forEach(function (item) {
    item.addEventListener("click", function (e) {
      
      // if (e.eventPhase === Event.BUBBLING_PHASE) {
      //   let aaa= item.querySelector("svg");
      //   if(aaa)
      // alert(aaa.nodeValue)
      debugger;
        props.deleteQuestion(item.id);
      // }
      e.stopImmediatePropagation();
    });
  });

  return (
    <Accordion>
      {props.data.map((x: SearchResponse) => (
        <Accordion.Item eventKey={`eventKey${x.mapperId}`}>
          <Accordion.Header>
            <Container>
              <Row>
                <Col>
                  <strong>{x.subject}</strong> - {x.question}{" "}
                </Col>
                <Col></Col>
                <Col placeholder="delete question" cursor="pointer">
                  <BsFillTrashFill
                    id={`${x.mapperId}`}
                    className="float-end"
                    color="red"
                    title="Delete Question"
                  />
                </Col>
              </Row>
            </Container>
          </Accordion.Header>
          <Accordion.Body>{parse(x.answer)}</Accordion.Body>
        </Accordion.Item>
      ))}
    </Accordion>
  );
}

export default SearchResultAccordion;
