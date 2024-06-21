import React from "react";
import { TailSpin } from "react-loader-spinner";

interface Props {
  height: string;
  colour?: string;
}

const Spinner: React.FC<Props> = ({ height, colour }) => {
  return (
    <TailSpin
      visible={true}
      height={height}
      width={height}
      color={colour ? colour : "var(--primary)"}
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
