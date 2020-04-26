export interface MenuItem {
  link: string;
  icon: string;
  alt: string;
}

export const menuItems: MenuItem[] = [
  {
    link: '/',
    icon: 'home',
    alt: 'Go to the front page'
  },
  {
    link: '/clients',
    icon: 'computer',
    alt: 'See a list of connected clients'
  },
  {
    link: '/simulations',
    icon: 'control_point',
    alt: 'Create a new plant configuration'
  }
];
