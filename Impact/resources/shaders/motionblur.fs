#version 110

/*

    Copyright (c) 2015 Oliver Lau <ola@ct.de>

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.

*/

uniform sampler2D uTexture;
uniform float uBlur;
uniform float uRot;
uniform vec2 uV;
uniform vec2 uResolution;

varying mat2 vRot;
varying vec2 vTexCoord;

void main(void) {
  vec2 v = uV / 600.0;
  float blur = uBlur / uResolution.x;
  vec2 pos = vTexCoord;
  vec4 sum = texture2D(uTexture, pos);
  const int MaxIterations = 5;
  for (int i = 1; i < MaxIterations; ++i) {
    vec2 offset = v * (float(i) / float(MaxIterations - 1));
    sum += texture2D(uTexture, pos - offset * vRot);
  }
  gl_FragColor = sum / float(MaxIterations);
}
