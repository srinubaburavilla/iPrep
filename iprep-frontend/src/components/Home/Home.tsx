import { Container, Row } from "react-bootstrap";

function Home() {
  return (
    <>
      <Container className="d-flex justify-content-center mb-3 text-center mt-sm-5">
        <Row lg={8}>
          <h1>Welcome to iPrep</h1>
          <h4>Tool to Master Your Tech Skills</h4>
          <h5>By srinubabu.ravilla@gmail.com</h5>
          <h6>Technologies Used: C#.NET, .NET Entity Framework Core 6.0.10, React, SQL Server</h6>
        </Row>
      </Container>
    </>
  );
}

export default Home;
