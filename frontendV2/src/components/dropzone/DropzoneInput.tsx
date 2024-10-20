import { FC, HTMLAttributes, useCallback, useState } from 'react';
import Dropzone, { Accept, DropzoneProps, FileRejection } from 'react-dropzone';
import { toast } from 'sonner';
import { cn } from 'utils/utils';
import { GoUpload } from 'react-icons/go';
import { formatBytes } from 'utils/math';
import FileCard from 'components/dropzone/FileCard';
import { ScrollArea } from 'components/scrollArea';

function formatAcceptedFileTypes(acceptedFiles: Accept): string {
  return Object.values(acceptedFiles).flat().join(', ');
}

interface DropzoneInputProps extends HTMLAttributes<HTMLDivElement> {
  value?: File[];
  onValueChange?: (files: File[]) => void;
  onUpload?: (files: File[]) => Promise<void>;
  progresses?: Record<string, number>;
  accept?: DropzoneProps['accept'];
  maxSize?: DropzoneProps['maxSize'];
  maxFileCount?: DropzoneProps['maxFiles'];
  multiple?: boolean;
  disabled?: boolean;
}

const DropzoneInput: FC<DropzoneInputProps> = ({
  value,
  onValueChange,
  onUpload,
  progresses,
  accept = {
    'image/*': [],
  },
  maxSize = 1024 * 1024 * 2,
  maxFileCount = 1,
  multiple = false,
  disabled = false,
  className,
  ...dropzoneProps
}) => {
  const [files, setFiles] = useState<File[] | undefined>(value);

  const isDisabled = disabled;

  const onDrop = useCallback(
    (acceptedFiles: File[], rejectedFiles: FileRejection[]) => {
      console.log('files', files);
      console.log('acceptedFiles', acceptedFiles);

      if (
        !multiple &&
        maxFileCount === 1 &&
        acceptedFiles.length + rejectedFiles.length > 1
      ) {
        toast.error('Cannot upload more than 1 file at a time');
        return;
      }

      if (acceptedFiles.length > maxFileCount) {
        toast.error(`Cannot upload more than ${maxFileCount} files`);
        return;
      }

      const newFiles = acceptedFiles.map((file) =>
        Object.assign(file, {
          preview: URL.createObjectURL(file),
        }),
      );

      const updatedFiles =
        files && multiple ? [...files, ...newFiles] : newFiles;

      setFiles(updatedFiles);
      onValueChange?.(updatedFiles);

      if (rejectedFiles.length > 0) {
        rejectedFiles.forEach(({ file }) => {
          toast.error(`File ${file.name} was rejected`);
        });
      }

      if (
        onUpload &&
        updatedFiles.length > 0 &&
        updatedFiles.length <= maxFileCount
      ) {
        const target =
          updatedFiles.length > 0 ? `${updatedFiles.length} files` : 'file';

        toast.promise(onUpload(updatedFiles), {
          loading: `Uploading ${target}...`,
          success: () => {
            setFiles([]);
            return `${target} uploaded`;
          },
          error: `Failed to upload ${target}`,
        });
      }
    },

    [files, maxFileCount, multiple, onUpload, onValueChange],
  );

  function onRemove(index: number) {
    if (!files) return;
    const newFiles = files.filter((_, i) => i !== index);
    setFiles(newFiles);
    onValueChange?.(newFiles);
  }

  return (
    <div className="relative flex flex-col gap-6 overflow-hidden">
      <Dropzone
        onDrop={onDrop}
        accept={accept}
        maxSize={maxSize}
        maxFiles={maxFileCount}
        multiple={maxFileCount > 1 || multiple}
        disabled={isDisabled}
      >
        {({ getRootProps, getInputProps, isDragActive }) => (
          <div
            {...getRootProps()}
            className={cn(
              'group relative grid h-56 w-full cursor-pointer place-items-center rounded-lg border-2 border-dashed border-muted-foreground/25 px-5 py-2.5 text-center transition hover:bg-muted/25 hover:border-muted-foreground/50',
              'ring-offset-background focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-ring focus-visible:ring-offset-2',
              isDragActive && 'border-muted-foreground/50',
              isDisabled && 'pointer-events-none opacity-60',
              className,
            )}
            {...dropzoneProps}
          >
            <input {...getInputProps()} />
            {isDragActive ? (
              <div className="flex flex-col items-center justify-center gap-4 sm:px-5">
                <GoUpload
                  className="size-7 text-muted-foreground p-3"
                  aria-hidden="true"
                />
                <p className="font-medium text-muted-foreground">
                  Drop the files here
                </p>
              </div>
            ) : (
              <div className="flex flex-col items-center justify-center sm:px-5">
                <GoUpload
                  className="text-muted-foreground p-3 size-16"
                  aria-hidden="true"
                />
                <div className="flex flex-col gap-px">
                  <p className="font-medium text-muted-foreground">
                    Drag & drop files here, or click to select files
                  </p>
                  <p className="text-sm text-muted-foreground/70">
                    You can upload
                    {maxFileCount > 1
                      ? ` ${maxFileCount === Infinity ? 'multiple' : maxFileCount}
                      ${formatAcceptedFileTypes(accept)} files (${formatAcceptedFileTypes(accept)}) up to ${formatBytes(maxSize)}`
                      : ` a file (${formatAcceptedFileTypes(accept)}) up to ${formatBytes(maxSize)}`}
                  </p>
                </div>
              </div>
            )}
          </div>
        )}
      </Dropzone>
      {files?.length ? (
        <ScrollArea className="h-fit w-full">
          <div className="flex max-h-48 flex-col gap-4 p-3 hover:bg-input transition">
            {files?.map((file, index) => (
              <FileCard
                key={index}
                file={file}
                onRemove={() => onRemove(index)}
                progress={progresses?.[file.name]}
              />
            ))}
          </div>
        </ScrollArea>
      ) : null}
    </div>
  );
};

export default DropzoneInput;
