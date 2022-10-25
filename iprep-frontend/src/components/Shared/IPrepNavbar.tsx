import Container from "react-bootstrap/Container";
import Nav from "react-bootstrap/Nav";
import Navbar from "react-bootstrap/Navbar";
import {
  BrowserRouter as Router,
  Routes,
  Route,
  Link,
  BrowserRouter,
} from "react-router-dom";
import AddQuestions from "../AddQuestions/AddQuestions";
import Home from "../Home/Home";
import ImportQuestions from "../Import/ImportQuestions";
import ManageTechnologies from "../ManageTechnologies/ManageTechnologies";
import ReportManager from "../ReportManager/ReportManager";
import SearchQuestions from "../SearchQuestions/SearchQuestion";

function IPrepNavbar() {
  return (
    <BrowserRouter>
      <Navbar bg="dark" variant="dark">
        <Container>
          <Navbar.Brand href="#home">iPrep</Navbar.Brand>
          <Nav className="me-auto">
            <Nav.Link as={Link} to="/">
              Home
            </Nav.Link>
            <Nav.Link as={Link} to="/search">
              Search
            </Nav.Link>
            <Nav.Link as={Link} to="/addnewquestion">
              Add New Question
            </Nav.Link>
            <Nav.Link as={Link} to="/managetechnologies">
              Technologies
            </Nav.Link>
            <Nav.Link as={Link} to="/reports">
              Reports
            </Nav.Link>
            <Nav.Link as={Link} to="/importquestions">
              Import
            </Nav.Link>
          </Nav>
        </Container>
      </Navbar>
      <div>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="search" element={<SearchQuestions />} />
          <Route path="addnewquestion" element={<AddQuestions />} />
          <Route path="managetechnologies" element={<ManageTechnologies />} />
          <Route path="reports" element={<ReportManager />} />
          <Route path="importquestions" element={<ImportQuestions />} />
        </Routes>
      </div>
    </BrowserRouter>
  );
}

export default IPrepNavbar;
