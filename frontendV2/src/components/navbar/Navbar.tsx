import { FC } from 'react';
import Sheet, { SheetTrigger } from 'components/sheet';
import Button from 'components/button';
import MenuIcon from 'components/navbar/icons/MenuIcon';
import { SheetContent } from 'components/sheet/Sheet';
import { Link } from 'react-router-dom';
import { getPageData } from 'App/pagesData';
import { Page } from 'App/router.types';

const NavbarPages: Page[] = [Page.About, Page.Contact, Page.Login, Page.SignUp];

const Navbar: FC<{ scrolled: boolean }> = ({ scrolled }) => {
  return (
    <header
      className={`flex w-full font-butler font-medium lg:px-14 px-8 items-center z-10 justify-between transition-all duration-500 ease-out ${scrolled ? 'h-32 text-primary-foreground' : 'bg-background h-16'}`}
    >
      <Link
        to="#"
        className={`transition-all duration-500 ease-out font-medium ${scrolled ? 'text-6xl mb-2' : 'text-2xl'}`}
      >
        Koral
      </Link>
      <nav className="hidden lg:flex gap-24 transition-all duration-500 ease-out">
        {NavbarPages.map((page) => {
          const pageData = getPageData(page);

          if (!pageData) throw new Error('Page not found');

          return (
            <Link to={pageData.path} className="mr-6">
              {pageData.title}
            </Link>
          );
        })}
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
          {NavbarPages.map((page) => {
            const pageData = getPageData(page);

            if (!pageData) throw new Error('Page not found');

            return (
              <Link
                to={pageData.path}
                className="flex w-full items-center py-2 text-lg font-semibold"
              >
                {pageData.title}
              </Link>
            );
          })}
        </SheetContent>
      </Sheet>
    </header>
  );
};

export default Navbar;
