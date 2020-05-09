import {Gradient, Svg} from '@svgdotjs/svg.js';

function createGradient(svg: Svg, colors: [string, string], id: string): Gradient {
  const [from, to] = colors;
  return svg.gradient('linear', stop => {
    stop.stop({color: from, offset: '0%'});
    stop.stop({color: to, offset: '100%'})
    stop.id(id);
    stop.attr('x1', '0%');
    stop.attr('x2', '100%');
    stop.attr('y1', '0%');
    stop.attr('y2', '0%');
    stop.attr('gradientUnits', 'userSpaceOnUse');
  });
}

export function getShootGradient(svg: Svg): Gradient {
  const colors: [string, string] = ['#7CB342', '#33691E'];
  return createGradient(svg, colors, 'shootGradient');
}

export function getRootGradient(svg: Svg): Gradient {
  const colors: [string, string] = ['#6D4C41', '#4E342E'];
  return createGradient(svg, colors, 'rootGradient');
}
