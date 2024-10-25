import { JSX } from 'react/jsx-runtime';
import { SVGProps } from 'react';
import { RxHamburgerMenu } from 'react-icons/rx';

function MenuIcon(props: JSX.IntrinsicAttributes & SVGProps<SVGSVGElement>) {
  return <RxHamburgerMenu fontSize={props.fontSize} />;
}

export default MenuIcon;
