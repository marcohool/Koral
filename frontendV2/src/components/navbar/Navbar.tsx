import { FC } from 'react';
import Sheet, { SheetTrigger } from 'components/sheet';
import Button from 'components/button';
import MenuIcon from 'components/navbar/icons/MenuIcon';
import { SheetContent } from 'components/sheet/Sheet';
import { Link } from 'react-router-dom';

const Navbar: FC<{ scrolled: boolean }> = ({ scrolled }) => {
  return (
    <header
      className={`flex w-full lg:px-14 px-8 items-center z-10 justify-between transition-all duration-500 ease-out ${scrolled ? 'h-32 text-primary-foreground' : 'bg-background h-16'}`}
    >
      <Link
        to="#"
        className={`transition-all duration-500 ease-out ${scrolled && 'text-6xl mb-2'}`}
      >
        Koral
      </Link>
      <nav className="hidden lg:flex gap-20 transition-all duration-500 ease-out">
        <Link to="#" className="mr-6">
          About
        </Link>
        <Link to="#" className="mr-6">
          Contact
        </Link>
        <Link to="#" className="mr-6">
          Log in
        </Link>
        <Link to="#" className="mr-6">
          Sign up
        </Link>
      </nav>
      <Sheet modal={false}>
        <SheetTrigger asChild>
          <Button
            variant="outline"
            size="icon"
            className="lg:hidden border-0 shadow-none bg-transparent"
          >
            <MenuIcon className="h-6 w-6" />
          </Button>
        </SheetTrigger>
        <SheetContent side="right">
          <div className="grid gap-2 py-6">
            <Link
              to="#"
              className="flex w-full items-center py-2 text-lg font-semibold"
            >
              About
            </Link>
            <Link
              to="#"
              className="flex w-full items-center py-2 text-lg font-semibold"
            >
              Contact
            </Link>
            <Link
              to="#"
              className="flex w-full items-center py-2 text-lg font-semibold"
            >
              Log in
            </Link>
            <Link
              to="#"
              className="flex w-full items-center py-2 text-lg font-semibold"
            >
              Sign up
            </Link>
          </div>
        </SheetContent>
      </Sheet>
    </header>
  );
};

export default Navbar;
