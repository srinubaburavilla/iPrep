import { useState } from "react";
import Toast from "react-bootstrap/Toast";

function IPrepToast(props: any) {
  const [show, setShow] = useState(true);

  return (
    <Toast
      onClose={() => {
        setShow(false);
        props.setshowToast(false)
      }}
      show={show}
    >
      <Toast.Header>
        <img src="holder.js/20x20?text=%20" className="rounded me-2" alt="" />
        <strong className="me-auto">{props.header}</strong>
      </Toast.Header>
      <Toast.Body>{props.body}</Toast.Body>
    </Toast>
  );
}

export default IPrepToast;
