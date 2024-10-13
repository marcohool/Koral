import { FC, ReactNode } from 'react';
import { cn } from 'utils/utils';
import { buttonVariants } from 'components/button/Button';
import { GoArrowLeft } from 'react-icons/go';
import { Link } from 'react-router-dom';

const HomeButton: FC<{ contentOnLeft: boolean }> = ({ contentOnLeft }) => (
  <>
    <Link
      to="/"
      className={cn(
        buttonVariants({ variant: 'ghost' }),
        'absolute left-2 sm:left-4 md:left-6 lg:left-8 top-2 sm:top-4 md:top-6 lg:top-8 z-10',
        !contentOnLeft && 'lg:ml-[50%]',
        'group',
      )}
    >
      <GoArrowLeft className="size-5 transition-transform duration-300 ease-in-out group-hover:-translate-x-1" />
      <span className="ml-2 hidden sm:inline">Home</span>
    </Link>
  </>
);

const PageContent: FC<{ contentOnLeft?: boolean; children: ReactNode }> = ({
  contentOnLeft,
  children,
}) => (
  <div
    className={cn(
      'flex flex-col items-center justify-center lg:w-1/2 w-full fixed left-0 h-screen ',
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
}> = ({ children, imageSrc, contentOnLeft }) => {
  const imagePositionClass = contentOnLeft ? 'right-0' : 'left-0';

  return (
    <div className="flex relative overflow-hidden h-screen">
      <HomeButton contentOnLeft={contentOnLeft} />
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
