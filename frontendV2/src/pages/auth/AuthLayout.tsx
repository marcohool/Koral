import { FC, ReactNode } from 'react';

const AuthLayout: FC<{
  children: ReactNode;
  imageSrc: string;
  formOnLeft: boolean;
}> = ({ children, imageSrc, formOnLeft }) => {
  const imagePositionClass = formOnLeft ? 'right-0' : 'left-0';

  return (
    <div className="flex relative overflow-hidden h-screen">
      <div
        className={`flex-1 ${formOnLeft ? 'order-1' : 'order-2'} flex items-center justify-center lg:w-1/2 w-full fixed left-0 h-screen`}
      >
        {children}
      </div>
      <img
        src={`images/${imageSrc}`}
        className={`hidden lg:flex fixed ${imagePositionClass} w-1/2 h-screen object-cover`}
        alt="Layout Image"
      />
    </div>
  );
};

export default AuthLayout;
