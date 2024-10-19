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
import { GoUpload } from 'react-icons/go';
import DropzoneInput from 'components/dropzone/DropzoneInput';

const DropzoneDialog: FC<{ children?: ReactNode }> = ({ children }) => {
  const [open, setOpen] = useState(false);
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
        <DropzoneInput />
        <DialogFooter className="justify-between">
          <DialogClose asChild>
            <Button type="submit" className="w-25" variant="ghost">
              Cancel
            </Button>
          </DialogClose>
          <Button type="submit" className="w-24" disabled={true}>
            Upload
          </Button>
        </DialogFooter>
      </DialogContent>
    </Dialog>
  );
};

export default DropzoneDialog;
