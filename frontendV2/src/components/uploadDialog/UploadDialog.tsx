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
import { useAddUpload } from 'pages/uploads/useUploads';

const MAX_UPLOAD_SIZE = 1024 * 1024 * 10;
const ACCEPTED_FILES = {
  'image/jpeg': ['.jpg', '.jpeg'],
  'image/png': ['.png'],
};

const UploadDialog: FC<{
  children?: ReactNode;
}> = ({ children }) => {
  const [files, setFiles] = useState<File[]>([]);
  const { mutate } = useAddUpload();

  return (
    <Dialog>
      <DialogTrigger asChild>{children}</DialogTrigger>
      <DialogContent className="sm:max-w-[600px]">
        <form
          className="flex flex-col gap-6"
          onSubmit={(event) => {
            event.preventDefault();
            // eslint-disable-next-line @typescript-eslint/no-unused-expressions
            files.length === 1 && mutate(files[0]);
          }}
        >
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
          <DialogFooter className="flex justify-between">
            <DialogClose asChild>
              <Button type="submit" className="w-25" variant="ghost">
                Cancel
              </Button>
            </DialogClose>
            <Button type="submit" className="w-24" disabled={files.length == 0}>
              Upload
            </Button>
          </DialogFooter>
        </form>
      </DialogContent>
    </Dialog>
  );
};

export default UploadDialog;
