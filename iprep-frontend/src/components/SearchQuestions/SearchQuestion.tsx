import axios from "axios";
import { useState, useEffect } from "react";
import "react-bootstrap";
import { Container, Form } from "react-bootstrap";
import { SearchResponse } from "../../Models/SearchResponse";
import SearchResultAccordion from "../SearchResultAccordion/SearchResultAccordion";
import IPrepToast from "../Shared/IPrepToast";

function SearchQuestions() {
  const searchQuestionsApiBaseUrl = "http://localhost:7253/ManageQuestions";

  const [searchResults, setSearchResults] = useState<SearchResponse[]>([]);
  const [filteredResults, setFilteredResults] = useState<SearchResponse[]>([]);
  const [showToast, setshowToast] = useState(false);

  useEffect(() => {
    loadQuestions();
  }, []);

  function loadQuestions() {
    axios
      .get(`${searchQuestionsApiBaseUrl}\\getall`)
      .then((response) => {
        setSearchResults(response.data);
        setFilteredResults(response.data);
        setTimeout(() => {
          setshowToast(false)
        }, 2000);
      })
      .catch((error) => console.log(error));
  }

  function performSearch(searchTerm: string) {
    let splittedWords = searchTerm.split(" ").map((x) => x.toLocaleLowerCase());
    let result = searchResults.filter((x) =>
      splittedWords.some((y) => x.question.toLocaleLowerCase().includes(y))
    );
    setFilteredResults(result);
    console.log(result);
  }

  const deleteQuestion = (id: string) => {
    axios
      .delete(`${searchQuestionsApiBaseUrl}\\delete\\${parseInt(id)}`)
      .then((response) => {
        setshowToast(true);
        loadQuestions();
      })
      .catch((error) => console.log(error));
  };

  return (
    <>
      <Container className="p-3">
        {showToast && (
          <IPrepToast header="Success" body="Question deleted successfully" />
        )}
        <br />
        <Form.Control
          type="text"
          id="inputPassword5"
          aria-describedby="passwordHelpBlock"
          placeholder="Search Questions"
          onChange={(e) => performSearch(e.target.value)}
        />
        <br />
        <SearchResultAccordion
          data={filteredResults}
          deleteQuestion={deleteQuestion}
        />
      </Container>
    </>
  );
}

export default SearchQuestions;
