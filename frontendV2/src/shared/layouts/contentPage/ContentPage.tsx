import { FC, ReactNode } from 'react';

const ContentPage: FC<{ heading?: string; content: ReactNode }> = ({
  heading,
  content,
}) => {
  return (
    <div className="flex flex-col gap-4 relative w-full">
      {heading && (
        <div className="mt-2 sm:mt-8 flex items-center w-full justify-center text-primary/[.6]">
          <p className="text-xs/loose">{heading}</p>
        </div>
      )}
      {content}
    </div>
  );
};

export default ContentPage;
