import { FC } from 'react';
import Sheet, { SheetTrigger } from 'components/sheet';
import Button from 'components/button';
import MenuIcon from 'components/navbar/icons/MenuIcon';
import { SheetContent } from 'components/sheet/Sheet';
import { Link } from 'react-router-dom';

const Navbar: FC = () => {
  return (
    <header className="flex h-20 w-full px-6 items-center z-10">
      <Sheet>
        <SheetTrigger asChild>
          <Button
            variant="outline"
            size="icon"
            className="lg:hidden md:hidden border-0 ml-auto shadow-none bg-transparent"
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
