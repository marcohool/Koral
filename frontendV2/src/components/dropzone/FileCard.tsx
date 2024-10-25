import Button from 'components/button';
import { Cross2Icon, FileTextIcon } from '@radix-ui/react-icons';
import { formatBytes } from 'utils/math';
import { FC } from 'react';
import Progress from 'components/progress';

function isFileWithPreview(file: File): file is File & { preview: string } {
  return 'preview' in file && typeof file.preview === 'string';
}

const FilePreview: FC<{ file: File & { preview: string } }> = ({ file }) => {
  if (file.type.startsWith('image/')) {
    return (
      <img
        src={file.preview}
        alt={file.name}
        width={48}
        height={48}
        loading="lazy"
        className="aspect-square shrink-0 rounded-md object-cover"
      />
    );
  }

  return (
    <FileTextIcon
      className="size-10 text-muted-foreground"
      aria-hidden="true"
    />
  );
};

const FileCard: FC<{ file: File; onRemove: () => void; progress?: number }> = ({
  file,
  progress,
  onRemove,
}) => {
  return (
    <div className="relative flex items-center gap-2.5">
      <div className="flex flex-1 gap-2.5 min-w-0">
        {isFileWithPreview(file) ? <FilePreview file={file} /> : null}
        <div className="flex w-full flex-col gap-2">
          <div className="flex flex-col gap-px">
            <p className="line-clamp-1 text-sm font-medium text-foreground/80 ">
              {file.name}
            </p>
            <p className="text-xs text-muted-foreground">
              {formatBytes(file.size)}
            </p>
          </div>
          {progress && <Progress value={progress} />}
        </div>
      </div>
      <div className="flex items-center gap-2">
        <Button
          type="button"
          variant="ghost"
          size="icon"
          className="size-7"
          onClick={onRemove}
        >
          <Cross2Icon className="size-4" aria-hidden="true" />
          <span className="sr-only">Remove file</span>
        </Button>
      </div>
    </div>
  );
};
export default FileCard;
