import { FC } from "react";

import NewUploadDisplay from "../NewUploadDisplay/NewUploadDisplay.tsx";

interface UploadModalProps {
  onClose: () => void;
}

const UploadModal: FC<UploadModalProps> = ({ onClose }) => {
  return (
    <div className="modal-backdrop" onClick={onClose}>
      <NewUploadDisplay onClose={onClose} isModal={true} />
    </div>
  );
};

export default UploadModal;
