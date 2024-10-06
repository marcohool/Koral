import {FC} from "react";
import Button from "components/button";

const Components: FC = () => {
  return (
    <div>
      Components
      <Button size="lg" variant="default" onClick={() => console.log("test")}>Click Here</Button>
    </div>
  );
};

export default Components