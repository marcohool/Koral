import { FC, ReactNode, useState } from 'react';
import {
  Dialog,
  DialogClose,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from 'components/dialog';
import Button from 'components/button';
import DropzoneInput from 'components/dropzone/DropzoneInput';

const MAX_UPLOAD_SIZE = 1024 * 1024 * 10;
const ACCEPTED_FILES = {
  'image/jpeg': ['.jpg', '.jpeg'],
  'image/png': ['.png'],
};

const UploadDialog: FC<{ children?: ReactNode }> = ({ children }) => {
  const [files, setFiles] = useState<File[]>([]);

  return (
    <Dialog>
      <DialogTrigger asChild>{children}</DialogTrigger>
      <DialogContent className="sm:max-w-[600px] gap-6">
        <DialogHeader>
          <DialogTitle className="text-2xl">Upload Image</DialogTitle>
          <DialogDescription>
            Drag and drop your image upload here or click to browse
          </DialogDescription>
        </DialogHeader>
        <DropzoneInput
          value={files}
          onValueChange={(files) => setFiles(files)}
          maxFileCount={1}
          maxSize={MAX_UPLOAD_SIZE}
          accept={ACCEPTED_FILES}
          className="h-64"
        />
        <DialogFooter className="justify-between">
          <DialogClose asChild>
            <Button type="submit" className="w-25" variant="ghost">
              Cancel
            </Button>
          </DialogClose>
          <Button type="submit" className="w-24" disabled={files.length == 0}>
            Upload
          </Button>
        </DialogFooter>
      </DialogContent>
    </Dialog>
  );
};

export default UploadDialog;
