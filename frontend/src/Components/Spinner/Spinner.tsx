import React from "react";
import { TailSpin } from "react-loader-spinner";

interface Props {}

const Spinner: React.FC<Props> = () => {
  return (
    <TailSpin
      visible={true}
      height="75%"
      width="75%"
      color="var(--background)"
      ariaLabel="tail-spin-loading"
      radius="1"
      wrapperStyle={{
        height: "inherit",
        display: "flex",
        alignItems: "center",
      }}
      wrapperClass="spinner-wrapper"
    />
  );
};

export default Spinner;
