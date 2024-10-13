import { FC, ReactNode } from 'react';
import { cn } from 'utils/utils';
import { GoArrowLeft } from 'react-icons/go';
import Button from 'components/button';

const RedirectButton: FC<{
  contentOnLeft: boolean;
  text?: string;
  onClick?: () => void;
}> = ({ contentOnLeft, text = 'Home', onClick }) => (
  <>
    <Button
      onClick={onClick}
      className={cn(
        'absolute left-2 sm:left-4 md:left-6 lg:left-8 top-2 sm:top-4 md:top-6 lg:top-8 z-10',
        !contentOnLeft && 'lg:ml-[50%]',
        'group',
      )}
      variant="ghost"
    >
      <GoArrowLeft className="size-5 transition-transform duration-300 ease-in-out group-hover:-translate-x-1" />
      <span className="ml-2 hidden sm:inline">{text}</span>
    </Button>
  </>
);

const PageContent: FC<{ contentOnLeft?: boolean; children: ReactNode }> = ({
  contentOnLeft,
  children,
}) => (
  <div
    className={cn(
      'flex flex-col items-center justify-center lg:w-1/2 w-full relative left-0 max-h-screen overflow-y-auto',
      !contentOnLeft && 'lg:ml-[50%]',
    )}
  >
    <div className="flex flex-col items-center px-4 w-max-[450px] w-full md:w-[500px] lg:px-16">
      {children}
    </div>
  </div>
);

const AuthLayout: FC<{
  children: ReactNode;
  imageSrc: string;
  contentOnLeft: boolean;
  redirect: { text: string; onClick: () => void };
}> = ({ children, imageSrc, contentOnLeft, redirect }) => {
  const imagePositionClass = contentOnLeft ? 'right-0' : 'left-0';

  return (
    <div className="flex relative overflow-hidden h-screen">
      <RedirectButton
        contentOnLeft={contentOnLeft}
        text={redirect.text}
        onClick={redirect.onClick}
      />
      <PageContent contentOnLeft={contentOnLeft}>{children}</PageContent>
      <img
        src={`images/${imageSrc}`}
        className={`hidden lg:flex fixed ${imagePositionClass} w-1/2 h-screen object-cover`}
        alt="Layout Image"
      />
    </div>
  );
};

export default AuthLayout;
