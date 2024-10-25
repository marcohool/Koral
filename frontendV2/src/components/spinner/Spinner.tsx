import { FC } from 'react';
import { ImSpinner8 } from 'react-icons/im';

const Spinner: FC<{ className: string }> = ({ className }) => {
  return <ImSpinner8 className={className} />;
};

export default Spinner;
